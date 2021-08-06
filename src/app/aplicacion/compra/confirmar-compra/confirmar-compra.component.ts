import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CompraService } from '../compra.service';
import {ConfirmarCompraModel} from './confirmarCompra.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-confirmar-compra',
  templateUrl: './confirmar-compra.component.html',
  styleUrls: ['./confirmar-compra.component.css']
})
export class ConfirmarCompraComponent implements OnInit {
  
   datosCompra: ConfirmarCompraModel = new ConfirmarCompraModel();
  
  constructor(private _compraService: CompraService,
              private _route: Router,
              private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this._compraService.getConfirmarCompra().subscribe(resp=>{
      this.datosCompra = resp;
    })
  }
  registrarCompra(){
    console.log('llegue')
    this._compraService.insertarCompra().subscribe(resp =>{
      if(resp.exito === 1){
        this._snackBar.open('Venta realizada con éxito!','',{
          duration:2500,
          verticalPosition:'top'
        })
      }
      else{
        this._snackBar.open('No se pudo realizar la venta!','',{
          duration:2500,
          verticalPosition:'top'
        })
      }
    });
    //this._route.navigate()
  }
}
