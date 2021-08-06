import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { CriptomonedaModel } from '../criptomonedas/criptomoneda.model';
import { CriptomonedasService } from '../criptomonedas/criptomonedas.service';
import { ConfirmarCompraModel } from './confirmar-compra/confirmarCompra.model';
import { CompraService } from './compra.service';
import { VentaService } from '../venta/venta.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-compra',
  templateUrl: './compra.component.html',
  styleUrls: ['./compra.component.css']
})
export class CompraComponent implements OnInit {

  listaCriptomonedas:CriptomonedaModel[];
  criptomoneda: CriptomonedaModel;
  estadoPrecioCompra: number;
  
  constructor(private _criptomonedasService: CriptomonedasService,
              private _route: Router,
              private _compraService: CompraService,
              private _ventaService: VentaService,
              private _snackBar: MatSnackBar) { }

  ngOnInit(): void {

    this._criptomonedasService.getCriptomonedas().subscribe(resp => {
      this.listaCriptomonedas = resp.data;
    });

    this._ventaService.estadoPrecioCompra().subscribe(resp =>{
      this.estadoPrecioCompra = resp.data.precioErrorStatus;
      console.log(this.estadoPrecioCompra);
    });

  }
  configurarCompra(idCripto: number){
    if(this.estadoPrecioCompra == 0)
   this._compraService.configurarCompra(idCripto);

   else{
    this._snackBar.open('Temporalmente fuera de servicio, intente de nuevo mas tarde. Gracias!',
                        '', {duration: 2500})
   }
  }
  
}

