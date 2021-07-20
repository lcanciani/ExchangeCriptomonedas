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
    UsuarioComponent
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
  providers: [CriptomonedasService,VentaService]
})
export class AplicacionModule { }
