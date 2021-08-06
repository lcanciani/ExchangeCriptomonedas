import { Component, OnInit, ViewChild } from '@angular/core';
import { LoginService } from '../login/login.service';
import { UsuarioService } from '../usuario/usuario.service';
import {DashboardModel} from './dashboard.model';
import {DashboardService} from './dashboard.service';
import {MovimientosUsuarioModel} from './movimientosUsuario.model';
import {MatTableDataSource} from '@angular/material/table';
import {MatPaginator} from '@angular/material/paginator';

 

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit 
{
  saldo: number ;
  //private _listMovimientosUsuario: MovimientosUsuarioModel[] = [];
  dataSourceTableMovimientos: MatTableDataSource<MovimientosUsuarioModel>;
  public listCriptosUsuario : DashboardModel[] = [];
  public columnas : string[] = ['id','nombre'];
  public colTableMov: string[]= ['fecha','tipo', 'nombre','cantidad','monto','precio'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(private _dashService: DashboardService,
              private _loginService: LoginService,
              private _usuarioService: UsuarioService){}

ngOnInit(){
  this._dashService.getDataForDashboard(this._loginService.usuarioData.idUsuario).subscribe(resp => {
    this.listCriptosUsuario = resp.data;
  });

  this._usuarioService.getSaldoFiat(this._loginService.usuarioData.idUsuario).subscribe(resp => {
    this.saldo = resp.data
  })

  this._dashService.getMovimientosUsuario(this._loginService.usuarioData.idUsuario).subscribe(resp => {
    this.dataSourceTableMovimientos = new MatTableDataSource(resp.data);
    this.dataSourceTableMovimientos.paginator = this.paginator;
  })
}
applyFilter(event: Event) {
  
  const filterValue = (event.target as HTMLInputElement).value;
  
  this.dataSourceTableMovimientos.filter = filterValue.trim().toLowerCase();

 if (this.dataSourceTableMovimientos.paginator) {
    this.dataSourceTableMovimientos.paginator.firstPage();
  }
}
}
