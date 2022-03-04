import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BancoModel } from '../bancos/banco.model';
import { BancosService } from '../bancos/bancos.service';
import { DashboardService } from '../dashboard/dashboard.service';
import { TipoMovimientoModel } from '../dashboard/tipoMovimiento.model';
import { DepositoModel } from './deposito.model';
import {DepositoService} from './deposito.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-deposito',
  templateUrl: './deposito.component.html',
  styleUrls: ['./deposito.component.css']
})
export class DepositoComponent implements OnInit {
  formDeposito: FormGroup =new FormGroup({});
  listBancos:BancoModel[];
  depositoModel: DepositoModel = new DepositoModel();
  
  tipoMovimiento: TipoMovimientoModel;
  comision: number = 0;
  constructor(private _formBuilder: FormBuilder,
              private _bancoService: BancosService,
              private _depositoService: DepositoService,
              private _dashBoardService:DashboardService,
              private _snackBar: MatSnackBar,
              private _route: Router) { }

  ngOnInit(): void {
    this.formDeposito = this._formBuilder.group({
      montoDeposito: ['', [Validators.required, 
        Validators.min(1),
        Validators.max(100000000)]]
      
    })
    this._bancoService.getBancos().subscribe(resp =>{
      this.listBancos = resp.data;
      
    })
    this._dashBoardService.getTipoMovimientoById(1).subscribe(resp=>{
      this.comision = resp.data.comision;
    })
  }
registrarDeposito(){
  
  this.depositoModel.monto = this.formDeposito.get('montoDeposito').value;
  this.depositoModel.idTipoMovimiento = 1;
  this.depositoModel.comision = this.comision;
  var usuario = JSON.parse(localStorage.getItem('usuario'))
  this.depositoModel.idUsuario = usuario.idUsuario;
  this.depositoModel.idBanco = null;
  

  this._depositoService.insertarDeposito(this.depositoModel).subscribe(resp => {
    if(resp.exito ===1){
      this._snackBar.open('Deposito realizado con éxito!','',{
        duration: 2000
      });
      this.formDeposito.reset();
      this._route.navigate(['/comp'])
    }

  });
}
}
