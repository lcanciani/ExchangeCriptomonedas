import { Component, OnInit } from '@angular/core';
import {DashboardModel} from './dashboard.model';
import {DashboardService} from './dashboard.service';

 

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit 
{
  saldo: number ;
  dashModel :DashboardModel;
  public listCriptosUsuario : DashboardModel[] = [];
  public columnas : string[] = ['id','nombre'];
  constructor(private _dashService: DashboardService){}

ngOnInit(){
  this._dashService.getDataForDashboard().subscribe(resp => {
    this.listCriptosUsuario = resp.data;
    this.dashModel = resp.data[1];
    this.saldo = this.dashModel.saldo;
    
  })
}

}
