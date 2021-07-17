import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { CriptomonedaModel } from '../../criptomonedas/criptomoneda.model';
import { VentaService } from '../venta.service';
import { ConfiguracionVenta } from './cofiguracionVenta.model';

@Component({
  selector: 'app-configurar-venta',
  templateUrl: './configurar-venta.component.html',
  styleUrls: ['./configurar-venta.component.css']
})
export class ConfigurarVentaComponent implements OnInit {
  private _formConfigurarVenta: FormGroup;
  private _criptoVenta: ConfiguracionVenta ;
  criptomonedaNombre: string ;
  comision: number;
  montoVenta: number;
  precioVenta: number;
  cantidadCripto: number;
  constructor(private _ventaService: VentaService,
              private _formBuilder: FormBuilder,
              private _route: Router) { }

  ngOnInit(): void {
    this._formConfigurarVenta = this._formBuilder.group({

    });

    this._ventaService.getCriptoAConfigurarVenta().subscribe(resp => {
      
      this.precioVenta = resp.data.precioVenta;
      
     this.criptomonedaNombre = resp.data.nombreCriptomoneda;
      this.comision = resp.data.comision;
    })
  }
  onSearchChange(monto: number){
    if(monto === undefined){
      this.cantidadCripto = undefined;
    }
    monto =  monto - (monto * this.comision);
    monto = monto / this.precioVenta;
    this.cantidadCripto = monto;
  }
  confirmarVenta(){
this._route.navigate(['confirmarVenta']);
  }
}
