import { Component, OnInit } from '@angular/core';
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
 
  
  constructor(private _criptomonedasService: CriptomonedasService,
              private _route: Router,
              private _ventaService: VentaService) { }

  ngOnInit(): void {

    this._criptomonedasService.getCriptomonedas().subscribe(resp => {
      this.listaCriptomonedas = resp.data;
    })
  }
  configurarVenta(idCripto: number){
   this._ventaService.configurarVenta(idCripto);
  }
  
}
