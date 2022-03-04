import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { CriptomonedaModel } from '../criptomonedas/criptomoneda.model';
import { CriptomonedasService } from '../criptomonedas/criptomonedas.service';
import { ConfirmarCompraModel } from './confirmar-compra/confirmarCompra.model';
import { CompraService } from './compra.service';
import { VentaService } from '../venta/venta.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DashboardService } from '../dashboard/dashboard.service';
import { DashboardModel } from '../dashboard/dashboard.model';
import { LoginService } from '../login/login.service';


@Component({
  selector: 'app-compra',
  templateUrl: './compra.component.html',
  styleUrls: ['./compra.component.css']
})
export class CompraComponent implements OnInit {

  listaCriptomonedas:CriptomonedaModel[] = [];
  listaCriptomonedasVenta: CriptomonedaModel[] = [];
  listaUsuarioCripto: DashboardModel[] = [];
  listaCriptoNombre: string[] = [];
  usuarioCripto: DashboardModel;
  criptomoneda: CriptomonedaModel;
  estadoPrecioCompra: number;
  
  constructor(private _criptomonedasService: CriptomonedasService,
              private _route: Router,
              private _compraService: CompraService,
              private _ventaService: VentaService,
              private _snackBar: MatSnackBar,
              private _dasboardService: DashboardService,
              private _loginService: LoginService) { }

  ngOnInit(): void {


    this._dasboardService.getDataForDashboard(this._loginService.usuarioData.idUsuario).subscribe(resp => {
      this.listaUsuarioCripto = resp.data
      
      this.listaUsuarioCripto.forEach(element => {
        if(element.cantidad !== null ){
         this.listaCriptoNombre.push(element.criptomoneda);            
        }
      });  

      this._criptomonedasService.getCriptomonedas().subscribe(resp => {


        resp.data.forEach(element => {
          if(!element.fechaBaja ){
            this.listaCriptomonedas.push(element);
          }       
          });
  
          this.listaCriptoNombre.forEach( nombreCripto => {
            this.listaCriptomonedas.forEach(cripto => {
              if(nombreCripto === cripto.nombre){
                this.listaCriptomonedasVenta.push(cripto);
              }
            });
  
  
        });
      
    });

    

      


      //this.listaCriptomonedas = resp.data;
    });

    this._ventaService.estadoPrecioCompra().subscribe(resp =>{
      this.estadoPrecioCompra = resp.data.precioErrorStatus;
      console.log(this.estadoPrecioCompra);
    });

  }
  configurarCompra(idCripto: number){
    if(this.estadoPrecioCompra == 0)
   this._compraService.configurarCompra(idCripto);

   else{
    this._snackBar.open('Temporalmente fuera de servicio, intente de nuevo mas tarde. Gracias!',
                        '', {duration: 2500})
   }
  }
  
}

