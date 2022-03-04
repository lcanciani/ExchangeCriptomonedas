import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { VentaService } from '../venta.service';
import {ConfirmarVentaModel} from './confirmarVenta.model';

@Component({
  selector: 'app-confirmar-venta',
  templateUrl: './confirmar-venta.component.html',
  styleUrls: ['./confirmar-venta.component.css']
})
export class ConfirmarVentaComponent implements OnInit {
  
   datosVenta: ConfirmarVentaModel;
  
  constructor(private _ventaService: VentaService,
              private _route: Router,
              private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this._ventaService.getConfirmarVenta().subscribe(resp=>{
      this.datosVenta = resp;
    })
  }
  registrarVenta(){
    console.log('llegue')
    this._ventaService.insertarVenta().subscribe(resp =>{
      if(resp.exito === 1){
        this._snackBar.open('Compra realizada con éxito','',{
          duration: 2000,
          verticalPosition:'top'
        })
      }
    });
    //this._route.navigate()
    this._route.navigate(['/comp'])
  }
}
