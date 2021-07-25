import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CompraService } from '../compra.service';
import {ConfirmarCompraModel} from './confirmarCompra.model';

@Component({
  selector: 'app-confirmar-compra',
  templateUrl: './confirmar-compra.component.html',
  styleUrls: ['./confirmar-compra.component.css']
})
export class ConfirmarCompraComponent implements OnInit {
  
   datosCompra: ConfirmarCompraModel = new ConfirmarCompraModel();
  
  constructor(private _compraService: CompraService,
              private _route: Router) { }

  ngOnInit(): void {
    this._compraService.getConfirmarCompra().subscribe(resp=>{
      this.datosCompra = resp;
    })
  }
  registrarCompra(){
    console.log('llegue')
    this._compraService.insertarCompra().subscribe(resp =>{
      console.log('lleguex3')
      console.log(resp)
      
    });
    //this._route.navigate()
  }
}
