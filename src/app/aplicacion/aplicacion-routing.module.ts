import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {DashboardComponent} from './dashboard/dashboard.component';
import {VentaComponent} from './venta/venta.component';
import {CriptomonedasComponent} from './criptomonedas/criptomonedas.component';
import { ConfigurarVentaComponent } from './venta/configurar-venta/configurar-venta.component';
import {AuthGuard} from './login/auth.guard';
import { ConfirmarVentaComponent } from './venta/confirmar-venta/confirmar-venta.component';

const routes: Routes = [
  {path: '',  canActivate:[AuthGuard] ,children:[
    {path: '', component: DashboardComponent},
    {path: 'venta', component: VentaComponent},
    {path: 'configurarVenta', component: ConfigurarVentaComponent},
    {path: 'confirmarVenta', component: ConfirmarVentaComponent},
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
