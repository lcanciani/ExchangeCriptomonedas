import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CriptomonedasService } from '../../criptomonedas/criptomonedas.service';
import { CompraService } from '../compra.service';
import {ConfirmarCompraModel} from '../confirmar-compra/confirmarCompra.model'
import { CriptomonedaModel } from '../../criptomonedas/criptomoneda.model';
import {LoginService} from '../../login/login.service';
import {DashboardService} from '../../dashboard/dashboard.service';
import { DashboardModel } from '../../dashboard/dashboard.model';




interface FormaCompra {
  imgUrl: string;
  value: string;
  viewValue: string;
}

interface Car {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-configurar-compra',
  templateUrl: './configurar-compra.component.html',
  styleUrls: ['./configurar-compra.component.css']
})
export class ConfigurarCompraComponent implements OnInit {
  public formConfigurarCompra: FormGroup;
  datosCompra :ConfirmarCompraModel = new ConfirmarCompraModel();  
  cantidadCripto: number;
  monto: number;
  listCriptomonedas : CriptomonedaModel[]= [];
  selectedValue: string;
  selectedCar: string;
  imgUrl: string = '';
  nombreCripto: string;

  formaCompra: FormaCompra[] = [  
    {imgUrl:'https://media.istockphoto.com/vectors/button-with-flag-of-argentina-vector-id595149776?k=6&m=595149776&s=612x612&w=0&h=EdqdImEB_nNltjEMgYTaSTatuZ8PjeAUEcx0VktNWKc=', value: 'arg', viewValue: 'ARG'}
  ];

  cars: Car[] = [
    {value: 'volvo', viewValue: 'Volvo'},
    {value: 'saab', viewValue: 'Saab'},
    {value: 'mercedes', viewValue: 'Mercedes'},
  ];



public  selected = 'arg'
public validarCantidad = false ;
public invalidarForm: boolean = true;
public criptomonedasUsuario: DashboardModel[]= [] ;  
private _criptoUsuario: DashboardModel;  
  constructor(private _compraService: CompraService,
              private _formBuilder: FormBuilder,
              private _route: Router,
              private _criptomonedasService: CriptomonedasService,
              private _dashboardService: DashboardService,
              private _loginService: LoginService) { 
                this.datosCompra.nombreCriptomoneda = '';
                this.datosCompra.monto = undefined;
                this.datosCompra.comision = undefined;
              }

  ngOnInit(): void {
    this.formConfigurarCompra = this._formBuilder.group({
      
      cantidad:['',
      [Validators.required,Validators.min(1)]],
      monto:[{value: '', disabled: true}],
      formaCompra: [{value:''}]
    });
    
    this._compraService.getCriptoAConfigurarCompra().subscribe(resp => {
       this.datosCompra.nombreCriptomoneda = resp.data.nombreCriptomoneda;
       this.nombreCripto = resp.data.nombreCriptomoneda;
      this.datosCompra.comision = resp.data.comision;
      this.datosCompra.idTipoMovimiento = resp.data.idTipoMovimiento;
      this.datosCompra.idCriptomoneda = resp.data.idCriptomoneda;
      this.datosCompra.porcentajeGanancia = resp.data.porcentajeGanancia;
      this.datosCompra.precioCompra = resp.data.precioCompra;
      
     var usuario = JSON.parse( localStorage.getItem('usuario'));
       this.datosCompra.idUsuario = usuario.idUsuario
      this.datosCompra.cotizacionDolar = resp.data.cotizacionDolar;
     
      
      this._criptomonedasService.getCriptomonedas().subscribe(resp => {
        this.listCriptomonedas = resp.data;
        this.imgUrl = this.listCriptomonedas.find((element)=>{
          return element.nombre === this.datosCompra.nombreCriptomoneda
        }).imagenUrl;
        
        this.formaCompra.push({imgUrl:this.imgUrl,value:this.nombreCripto, viewValue:this.nombreCripto});
      })
      
    })
   
    this._dashboardService.getDataForDashboard(this.datosCompra.idUsuario).subscribe(resp => {
        
        
      this.criptomonedasUsuario = resp.data
      
       this.criptomonedasUsuario.forEach(element => {
         if(element.criptomoneda == this.datosCompra.nombreCriptomoneda){
          this._criptoUsuario = element;
             
         }
       });      
    });

  }
/*
   multiplo5(control: AbstractControl): ValidationErrors| null {
    let nro = parseInt(control.value);
    if (nro <= this.validarCantidad)
        return null;
    else
        return { multiplo5: true }
}
*/

  cambioFormaPago(){
    let cantidad = this.formConfigurarCompra.get('cantidad').value; 
    
    if(this.formConfigurarCompra.get('formaCompra').value === 'arg'){
      this.onSearchChange(cantidad);

      this.monto = this.datosCompra.precioCompra * this.datosCompra.cotizacionDolar;

      this.monto = this.monto-(this.monto * this.datosCompra.comision);
      
      this.monto = cantidad / this.monto;
      this.datosCompra.monto = this.monto;       
      
    }
    else{
      this.onSearchChange(cantidad);
    
    this.datosCompra.cantidad = cantidad;
     this.monto = (cantidad * this.datosCompra.precioCompra);
    this.monto =  this.monto - (this.monto * this.datosCompra.comision);
    this.monto = this.monto *this.datosCompra.cotizacionDolar;
     this.datosCompra.monto = this.monto;
     
    }
  }


  onSearchChange(cantidad: number){
    
   if(this.formConfigurarCompra.get('cantidad').value === null){
    this.datosCompra.monto = 0;
    this.invalidarForm = true; 
   }
   else{

    if(this.formConfigurarCompra.get('formaCompra').value === 'arg'){
      
      
      this.monto = this.datosCompra.precioCompra * this.datosCompra.cotizacionDolar;

      if((this.monto*this._criptoUsuario.cantidad)>cantidad){
        this.validarCantidad = false;
        this.invalidarForm = false; 
      }
      else{
        this.validarCantidad = true;
        this.invalidarForm = true;
      }
      
      this.monto = this.monto-(this.monto * this.datosCompra.comision);
      this.monto = cantidad / this.monto;
      this.datosCompra.monto = this.monto;
    }
    


    if(this.formConfigurarCompra.get('formaCompra').value === this.datosCompra.nombreCriptomoneda){
      
      if( this._criptoUsuario.cantidad < cantidad ){
        this.validarCantidad = true;
        this.invalidarForm = true;
      }
     // if(cantidad <= this._criptoUsuario.cantidad)
     else{
      this.validarCantidad = false;
      this.invalidarForm  = false;
     }
      

    
    this.datosCompra.cantidad = cantidad;
     this.monto = (cantidad * this.datosCompra.precioCompra);
    this.monto =  this.monto - (this.monto * this.datosCompra.comision);
    this.monto = this.monto *this.datosCompra.cotizacionDolar;
     this.datosCompra.monto = this.monto;
    }
  }
  }
  confirmarCompra(){  
this._compraService.confirmarCompra(this.datosCompra);

  }
}

