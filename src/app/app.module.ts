import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {MatDialogModule} from '@angular/material/dialog';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MatButtonModule } from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
//import { DashboardComponent } from './aplicacion/dashboard/dashboard.component';
//import { UsuarioComponent } from './aplicacion/usuario/usuario.component';
import {HttpClientModule} from '@angular/common/http';
import {MatTableModule} from '@angular/material/table';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatSnackBarModule} from '@angular/material/snack-bar';
//import { CriptomonedasComponent } from './aplicacion/criptomonedas/criptomonedas.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {AplicacionComponent} from 'src/app/aplicacion/aplicacion.component'
//import { CriptomonedasService } from './aplicacion/criptomonedas/criptomonedas.service';
//import { ListaCriptoComponent } from './aplicacion/criptomonedas/lista-cripto/lista-cripto.component';
//import { FormCriptoComponent } from './aplicacion/criptomonedas/form-cripto/form-cripto.component';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { LoginComponent } from './aplicacion/login/login.component';
import { DeslogearComponent } from './aplicacion/deslogear/deslogear.component';
//import { VentaComponent } from './aplicacion/venta/venta.component';
//import { NavbarComponent } from './aplicacion/navbar/navbar.component';
//import { ConfigurarVentaComponent } from './aplicacion/venta/configurar-venta/configurar-venta.component';
//import { VentaService } from './aplicacion/venta/venta.service';
import {MatCardModule} from '@angular/material/card';
//import { ConfirmarVentaComponent } from './aplicacion/venta/confirmar-venta/confirmar-venta.component';
import {MatIconModule} from '@angular/material/icon';
import {MatToolbarModule} from '@angular/material/toolbar';
import { AplicacionModule } from './aplicacion/aplicacion.module';
import { SharedModule } from './shared/shared.module';


@NgModule({
  declarations: [
    AppComponent,
    
    HeaderComponent,
    FooterComponent,
    AplicacionComponent,
    
    LoginComponent,
    
    
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule, 
    HttpClientModule,
   SharedModule,
    AplicacionModule
  ],
  exports:[],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
