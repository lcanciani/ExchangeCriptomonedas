import { Component, OnInit } from '@angular/core';
import {ConfirmarVentaModel} from './confirmarVenta.model';

@Component({
  selector: 'app-confirmar-venta',
  templateUrl: './confirmar-venta.component.html',
  styleUrls: ['./confirmar-venta.component.css']
})
export class ConfirmarVentaComponent implements OnInit {
  confVentaModel:ConfirmarVentaModel;
  constructor() { }

  ngOnInit(): void {
  }

}
