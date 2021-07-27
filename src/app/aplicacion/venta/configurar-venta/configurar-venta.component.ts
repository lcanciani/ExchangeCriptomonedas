import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ConfirmarVentaModel } from '../confirmar-venta/confirmarVenta.model';
import { VentaService } from '../venta.service';
import {LoginService} from '../../login/login.service';


@Component({
  selector: 'app-configurar-venta',
  templateUrl: './configurar-venta.component.html',
  styleUrls: ['./configurar-venta.component.css']
})
export class ConfigurarVentaComponent implements OnInit {
  public formConfigurarVenta: FormGroup;
  datosVenta :ConfirmarVentaModel = new ConfirmarVentaModel();  
  cantidadCripto: number;
    
  constructor(private _ventaService: VentaService,
              private _formBuilder: FormBuilder,
              private _route: Router,
              private _loginService: LoginService) { }

  ngOnInit(): void {
    this.formConfigurarVenta = this._formBuilder.group({
      monto:['',Validators.min(1)],
      cantidad:[{value:'', disabled: true}]
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
    })
  }
  
  onSearchChange(monto: number){
    if(monto === undefined){
      this.datosVenta.cantidad = undefined;
    }
    this.datosVenta.monto = monto;
    monto = monto - (monto * this.datosVenta.comision);
    
    this.datosVenta.cantidad = monto / this.datosVenta.precioVenta;
      
  }
  confirmarVenta(){  
this._ventaService.confirmarVenta(this.datosVenta);
this._route.navigate(['comp/confirmarVenta']);
  }
}
