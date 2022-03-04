import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ConfirmarVentaModel } from '../confirmar-venta/confirmarVenta.model';
import { VentaService } from '../venta.service';
import {LoginService} from '../../login/login.service';
import { CriptomonedaModel } from '../../criptomonedas/criptomoneda.model';
import { UsuarioService } from '../../usuario/usuario.service';
import { DashboardService } from '../../dashboard/dashboard.service';
import { CriptomonedasService } from '../../criptomonedas/criptomonedas.service';
interface FormaCompra {
  imgUrl: string;
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-configurar-venta',
  templateUrl: './configurar-venta.component.html',
  styleUrls: ['./configurar-venta.component.css']
})
export class ConfigurarVentaComponent implements OnInit {
  public formConfigurarVenta: FormGroup;
  datosVenta :ConfirmarVentaModel = new ConfirmarVentaModel();  
  cantidadCripto: number;
  selectedValue: string;
  imgUrl: string = '';
  nombreCripto: string;
  listCriptomonedas: CriptomonedaModel[] = [];
  monto: number;
  formaCompra: FormaCompra[] = [{imgUrl:'https://media.istockphoto.com/vectors/button-with-flag-of-argentina-vector-id595149776?k=6&m=595149776&s=612x612&w=0&h=EdqdImEB_nNltjEMgYTaSTatuZ8PjeAUEcx0VktNWKc=', value: 'arg', viewValue: 'ARG'}];  
  selected = 'arg';
  validarCantidad = false;
  invalidarForm = true;
  saldoUsuario: number; 
  //valor que va a aparece en el input cantidad
  cantidadCompraCripto: number;



  constructor(private _ventaService: VentaService,
              private _formBuilder: FormBuilder,
              private _route: Router,
              private _loginService: LoginService,
              private _dashService: DashboardService,
              private _usuarioService: UsuarioService,
              private _criptomonedasService: CriptomonedasService
              ) { }

  ngOnInit(): void {
    this.formConfigurarVenta = this._formBuilder.group({
      monto:['',Validators.min(1)],
      cantidad:[{value:'', disabled: true}],
      formaCompra: [{value:''}]
    });

    this._ventaService.getCriptoAConfigurarVenta().subscribe(resp => {
       this.datosVenta.nombreCriptomoneda = resp.data.nombreCriptomoneda;
      this.datosVenta.comision = resp.data.comision;
      this.datosVenta.idTipoMovimiento = resp.data.idTipoMovimiento;
      this.datosVenta.idCriptomoneda = resp.data.idCriptomoneda;
      this.datosVenta.porcentajeGanancia = resp.data.porcentajeGanancia;
      this.datosVenta.precioVenta = resp.data.precioVenta;
       this.datosVenta.idUsuario = this._loginService.usuarioData.idUsuario
      this.datosVenta.cotizacionDolar = 0;
    });

    this._criptomonedasService.getCriptomonedas().subscribe(resp => {
      this.listCriptomonedas = resp.data;
      this.imgUrl = this.listCriptomonedas.find(element => {
        return element.nombre === this.datosVenta.nombreCriptomoneda
      }).imagenUrl;
      this.formaCompra.push({imgUrl:this.imgUrl,value:this.datosVenta.nombreCriptomoneda, viewValue:this.datosVenta.nombreCriptomoneda});
    })
/*
    this._criptomonedasService.getCriptomonedas().subscribe(resp => {
      this.listCriptomonedas = resp.data;
      this.imgUrl = this.listCriptomonedas.find((element)=>{
        return element.nombre === this.datosVenta.nombreCriptomoneda
      }).imagenUrl;
*/
  }
  
  /*
  cambioFormaPago(){
    let cantidad = this.formConfigurarVenta.get('monto').value; 
    
    if(this.formConfigurarVenta.get('formaCompra').value === 'arg'){
      this.onSearchChange(cantidad);

      this.monto = this.datosVenta.precioVenta * this.datosVenta.cotizacionDolar;

      this.monto = this.monto-(this.monto * this.datosVenta.comision);
      
      this.monto = cantidad / this.monto;
      this.datosVenta.monto = this.monto;       
      
    }
    else{
      this.onSearchChange(cantidad);
    
    this.datosVenta.cantidad = cantidad;
     this.monto = (cantidad * this.datosVenta.precioVenta);
    this.monto =  this.monto - (this.monto * this.datosVenta.comision);
    this.monto = this.monto *this.datosVenta.cotizacionDolar;
     this.datosVenta.monto = this.monto;
     
    }
  }
*/

cambioFormaPago(){
  let cantidad = this.formConfigurarVenta.get('monto').value; 
  this.onSearchChange(cantidad);
} 


  onSearchChange(monto: number){
    if(this.formConfigurarVenta.get('cantidad').value === null){
      this.datosVenta.monto = 0;
      this.invalidarForm = true; 
     }
     else{
       if(this.formConfigurarVenta.get('formaCompra').value === 'arg'){
        this._usuarioService.getSaldoFiat(this._loginService.usuarioData.idUsuario).subscribe(resp => {
          this.saldoUsuario = resp.data
          
        })

        if(monto > this.saldoUsuario){
          this.validarCantidad = true;
          this.invalidarForm = true; 
        }

        else{
          this.validarCantidad = false;
          this.invalidarForm = false;
        }

        this.cantidadCompraCripto = monto -(monto * this.datosVenta.comision);
        this.cantidadCompraCripto = this.cantidadCompraCripto / (this.datosVenta.precioVenta);
        this.datosVenta.cantidad = this.cantidadCompraCripto;
        this.datosVenta.monto = monto;
        //datosVenta.monto es el total de la factura
        
       }
       /*
       this.datosVenta.monto = monto;
     monto = monto - (monto * this.datosVenta.comision);
     
     this.datosVenta.cantidad = monto / this.datosVenta.precioVenta;
     */
     }

     if(this.formConfigurarVenta.get('formaCompra').value === this.datosVenta.nombreCriptomoneda){
      this._usuarioService.getSaldoFiat(this._loginService.usuarioData.idUsuario).subscribe(resp => {
        this.saldoUsuario = resp.data
        
      })
     this.monto = monto * this.datosVenta.precioVenta;
     
      if( this.monto > this.saldoUsuario ){
        
        this.validarCantidad = true;
        this.invalidarForm = true;
      }
     // if(cantidad <= this._criptoUsuario.cantidad)
     else{
      this.validarCantidad = false;
      this.invalidarForm  = false;
     }

      this.cantidadCompraCripto = this.monto;
      this.datosVenta.cantidad = monto;
    
    
     this.datosVenta.monto = this.monto;
     }  
  }

  confirmarVenta(){  
this._ventaService.confirmarVenta(this.datosVenta);
this._route.navigate(['comp/confirmarVenta']);
  }
}
