import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {DashboardComponent} from './dashboard/dashboard.component';
import {VentaComponent} from './venta/venta.component';
import {CriptomonedasComponent} from './criptomonedas/criptomonedas.component';
import { ConfigurarVentaComponent } from './venta/configurar-venta/configurar-venta.component';
import {AuthGuard} from './login/auth.guard';
import { ConfirmarVentaComponent } from './venta/confirmar-venta/confirmar-venta.component';
import { DepositoComponent } from './deposito/deposito.component';
import { ConfirmarCompraComponent } from './compra/confirmar-compra/confirmar-compra.component';
import { ConfigurarCompraComponent } from './compra/configurar-compra/configurar-compra.component';
import { CompraComponent } from './compra/compra.component';
import { ExtraccionComponent } from './extraccion/extraccion.component';
import { MailComponent } from './mail/mail.component';
import { EstadisticasComponent } from './estadisticas/estadisticas.component';
import { RegistrarUsuarioComponent } from './usuario/registrar-usuario/registrar-usuario.component';

const routes: Routes = [
  {path: '',  canActivate:[AuthGuard] ,children:[
    {path: '', component: DashboardComponent},
    {path: 'venta', component: VentaComponent},
    {path: 'configurarVenta', component: ConfigurarVentaComponent},
    {path: 'confirmarVenta', component: ConfirmarVentaComponent},

    {path: 'compra', component: CompraComponent},
    {path: 'configurarCompra', component: ConfigurarCompraComponent},
    {path: 'confirmarCompra', component: ConfirmarCompraComponent},

    {path: 'enviarMail', component: MailComponent},
    {path: 'estadisticas', component: EstadisticasComponent},
    

    {path: 'extraccion', component: ExtraccionComponent},
    {path: 'deposito', component: DepositoComponent},
    {path: 'editar', component:CriptomonedasComponent},
    {path: 'dashboard', component: DashboardComponent },
    {path: '**', component: DashboardComponent }
  ]
},];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AplicacionRoutingModule { }
