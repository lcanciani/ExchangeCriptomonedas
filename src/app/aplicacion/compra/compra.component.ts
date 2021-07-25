import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { CriptomonedaModel } from '../criptomonedas/criptomoneda.model';
import { CriptomonedasService } from '../criptomonedas/criptomonedas.service';
import { ConfirmarCompraModel } from './confirmar-compra/confirmarCompra.model';
import { CompraService } from './compra.service';

@Component({
  selector: 'app-compra',
  templateUrl: './compra.component.html',
  styleUrls: ['./compra.component.css']
})
export class CompraComponent implements OnInit {

  listaCriptomonedas:CriptomonedaModel[];
  criptomoneda: CriptomonedaModel;
 
  
  constructor(private _criptomonedasService: CriptomonedasService,
              private _route: Router,
              private _compraService: CompraService) { }

  ngOnInit(): void {

    this._criptomonedasService.getCriptomonedas().subscribe(resp => {
      this.listaCriptomonedas = resp.data;
    })
  }
  configurarCompra(idCripto: number){
   this._compraService.configurarCompra(idCripto);
   
  }
  
}

