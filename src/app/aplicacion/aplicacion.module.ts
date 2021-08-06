import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
//routing
import { AplicacionRoutingModule } from './aplicacion-routing.module';
//components
import { VentaComponent } from './venta/venta.component';
import { ConfigurarVentaComponent } from './venta/configurar-venta/configurar-venta.component';
import { ConfirmarVentaComponent } from './venta/confirmar-venta/confirmar-venta.component';
import { CriptomonedasComponent } from './criptomonedas/criptomonedas.component';
import { FormCriptoComponent } from './criptomonedas/form-cripto/form-cripto.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UsuarioComponent } from './usuario/usuario.component';
import { CriptomonedasService } from './criptomonedas/criptomonedas.service';
import { VentaService } from './venta/venta.service';
import {NavbarComponent} from './navbar/navbar.component';
import {DeslogearComponent} from './deslogear/deslogear.component';
import {ListaCriptoComponent} from './criptomonedas/lista-cripto/lista-cripto.component';
//shared
import {SharedModule} from '../shared/shared.module';
import { DepositoComponent } from './deposito/deposito.component';
import { DashboardService } from './dashboard/dashboard.service';
import { BancosComponent } from './bancos/bancos.component';
import { CompraComponent } from './compra/compra.component';
import { ConfigurarCompraComponent } from './compra/configurar-compra/configurar-compra.component';
import { ConfirmarCompraComponent } from './compra/confirmar-compra/confirmar-compra.component';
import { ExtraccionComponent } from './extraccion/extraccion.component';
import { ExtraccionService } from './extraccion/extraccion.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import {JwtInterceptors} from './security/jwt.interceptor';
import { MailComponent } from './mail/mail.component';
import { MailService } from './mail/mail.service';
import { EstadisticasComponent } from './estadisticas/estadisticas.component';


@NgModule({
  declarations: [
    
    NavbarComponent,
    DeslogearComponent,
    VentaComponent,
    ConfigurarVentaComponent,
    ConfirmarVentaComponent,
    CriptomonedasComponent,
    FormCriptoComponent,
    ListaCriptoComponent,
    DashboardComponent,
    UsuarioComponent,
    DepositoComponent,
    BancosComponent,
    CompraComponent,
    ConfigurarCompraComponent,
    ConfirmarCompraComponent,
    ExtraccionComponent,
    MailComponent,
    EstadisticasComponent,
    
  ],
  imports: [
    CommonModule,
    AplicacionRoutingModule,
    SharedModule
    
  ],
  exports:[
    DeslogearComponent,
    NavbarComponent,
    AplicacionRoutingModule,
    VentaComponent,
    ConfigurarVentaComponent,
    ConfirmarVentaComponent,
    CriptomonedasComponent,
    FormCriptoComponent,
    ListaCriptoComponent,
    DashboardComponent,
    UsuarioComponent
  ],
  providers: [MailService,
              CriptomonedasService,
              VentaService,
              DashboardService,
              ExtraccionService,{
    provide: HTTP_INTERCEPTORS, useClass: JwtInterceptors, multi: true
  }]
})
export class AplicacionModule { }
