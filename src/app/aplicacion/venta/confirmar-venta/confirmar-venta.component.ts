import { Component, OnInit } from '@angular/core';
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
              private _route: Router) { }

  ngOnInit(): void {
    this._ventaService.getConfirmarVenta().subscribe(resp=>{
      this.datosVenta = resp;
    })
  }
  registrarVenta(){
    console.log('llegue')
    this._ventaService.insertarVenta().subscribe(resp =>{
      console.log('lleguex3')
      console.log(resp)
    });
    //this._route.navigate()
  }
}
