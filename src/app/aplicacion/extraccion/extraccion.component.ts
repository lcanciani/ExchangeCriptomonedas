import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DashboardService } from '../dashboard/dashboard.service';
import { ExtraccionService } from './extraccion.service';
import {BancosUsuario} from './bancosUsuario.model';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ExtraccionModel } from './extraccion.model';

@Component({
  selector: 'app-extraccion',
  templateUrl: './extraccion.component.html',
  styleUrls: ['./extraccion.component.css']
})
export class ExtraccionComponent implements OnInit {
  public comision: number = 0;
  public saldoUsuario: number = 0;
  public listBancosUsuario: BancosUsuario[];
  public formExtraccion: FormGroup;
  public validarMonto: boolean = true;
  public extraccionModel: ExtraccionModel = new ExtraccionModel();
  private  _usuario; 
  montoUser: number;
  constructor(private _extraccionService: ExtraccionService,
              private _dashBoardService: DashboardService,
              private _formBuilder: FormBuilder) { 
                this._usuario = JSON.parse( localStorage.getItem('usuario')); 
                //console.log( 'aaaaaaaaaa'+this._usuario.idUsuario)
              }

  ngOnInit(): void {
    this.formExtraccion = this._formBuilder.group({
      montoExtraccion:[{value:''}],
      bancoUsuario: []
    });

      
      this._extraccionService.getDatosExtraccion(this._usuario.idUsuario).subscribe(resp => {
        this.saldoUsuario = resp.data.saldoUsuario;
        console.log(this.saldoUsuario)
        this.listBancosUsuario = resp.data.listBancosUsuario
        console.log(this.listBancosUsuario)
      })

      this._dashBoardService.getTipoMovimientoById(2).subscribe(resp => {
        this.comision = resp.data.comision;
        console.log(this.comision)
      });
  }
  registrarExtraccion(){
    this.extraccionModel.comision = this.comision;
    this.extraccionModel.idBanco = this.formExtraccion.get('bancoUsuario').value;
    this.extraccionModel.idUsuario = this._usuario.idUsuario;
    this.extraccionModel.idTipoMovimiento = 2;
    this.extraccionModel.monto = this.montoUser;
  this._extraccionService.insertarExtraccion(this.extraccionModel).subscribe(resp => {
      console.log(resp);
    });
  }
  onSearchChange(monto: number){
    
    if(monto<=this.saldoUsuario){
      this.montoUser = monto-(monto*this.comision);
      
      this.validarMonto = true;
    }
    else{
      this.validarMonto = false;
    }
  }
}
