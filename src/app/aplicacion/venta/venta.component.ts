import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { CriptomonedaModel } from '../criptomonedas/criptomoneda.model';
import { CriptomonedasService } from '../criptomonedas/criptomonedas.service';
import { ConfirmarVentaModel } from './confirmar-venta/confirmarVenta.model';
import { VentaService } from './venta.service';

@Component({
  selector: 'app-venta',
  templateUrl: './venta.component.html',
  styleUrls: ['./venta.component.css']
})
export class VentaComponent implements OnInit {

  listaCriptomonedas:CriptomonedaModel[];
  criptomoneda: CriptomonedaModel;
 estadoPrecioCompra: number;
  
  constructor(private _criptomonedasService: CriptomonedasService,
              private _route: Router,
              private _ventaService: VentaService,
              private _snackBar: MatSnackBar) { }

  ngOnInit(): void {

    this._criptomonedasService.getCriptomonedas().subscribe(resp => {
      this.listaCriptomonedas = resp.data;
    })
    

    this._ventaService.estadoPrecioCompra().subscribe(resp =>{
      this.estadoPrecioCompra = resp.data.precioErrorStatus;
      console.log(this.estadoPrecioCompra);
    });
  }
  configurarVenta(idCripto: number){
    if(this.estadoPrecioCompra == 0)
   this._ventaService.configurarVenta(idCripto);
   else{
    this._snackBar.open('Temporalmente fuera de servicio, intente de nuevo mas tarde. Gracias!',
                        '', {duration: 2500})
   }
  }
  
}
