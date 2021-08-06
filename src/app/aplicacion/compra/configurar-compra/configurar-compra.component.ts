import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CompraService } from '../compra.service';
import {ConfirmarCompraModel} from '../confirmar-compra/confirmarCompra.model'


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
    
  constructor(private _compraService: CompraService,
              private _formBuilder: FormBuilder,
              private _route: Router) { 
                this.datosCompra.nombreCriptomoneda = '';
                this.datosCompra.monto = undefined;
                this.datosCompra.comision = undefined;
              }

  ngOnInit(): void {
    this.formConfigurarCompra = this._formBuilder.group({
      
      cantidad:['',[Validators.required,Validators.min(1),Validators.max(1000000000)]],
      monto:[{value: '', disabled: true}]
    });

    this._compraService.getCriptoAConfigurarCompra().subscribe(resp => {
       this.datosCompra.nombreCriptomoneda = resp.data.nombreCriptomoneda;
      this.datosCompra.comision = resp.data.comision;
      this.datosCompra.idTipoMovimiento = resp.data.idTipoMovimiento;
      this.datosCompra.idCriptomoneda = resp.data.idCriptomoneda;
      this.datosCompra.porcentajeGanancia = resp.data.porcentajeGanancia;
      this.datosCompra.precioCompra = resp.data.precioCompra;
      
     var usuario = JSON.parse( localStorage.getItem('usuario'));
       this.datosCompra.idUsuario = usuario.idUsuario
      this.datosCompra.cotizacionDolar = resp.data.cotizacionDolar;
      console.log(this.datosCompra.cotizacionDolar)
    })
  }
  
  onSearchChange(cantidad: number){
    if(cantidad === undefined){
      this.datosCompra.monto = undefined;
    }
    this.datosCompra.cantidad = cantidad;
     this.monto = (cantidad * this.datosCompra.precioCompra);
    this.monto =  this.monto - (this.monto * this.datosCompra.comision);
    this.monto = this.monto *this.datosCompra.cotizacionDolar;
     this.datosCompra.monto = this.monto;
  }
  confirmarCompra(){  
this._compraService.confirmarCompra(this.datosCompra);

  }
}

