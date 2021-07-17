import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AplicacionComponent} from './aplicacion.component';
import {DashboardComponent} from './dashboard/dashboard.component';
import {VentaComponent} from './venta/venta.component';
import {CriptomonedasComponent} from './criptomonedas/criptomonedas.component';

const routes: Routes = [
  {path: '', component: AplicacionComponent, children:[
    {path: '', component: DashboardComponent},
    {path: 'venta', component: VentaComponent},
    {path: 'editar', component:CriptomonedasComponent}
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AplicacionRoutingModule { }
