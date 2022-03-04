import { AfterViewInit, Component, ViewChild, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators  } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UsuarioService } from '../usuario/usuario.service';
import {MailService} from './mail.service';
import { MailRequest } from './mailRequest.model';
import {UsuarioModel} from '../usuario/usuario.model';
import {MatTableDataSource} from '@angular/material/table';
import {MatPaginator} from '@angular/material/paginator';



@Component({
  selector: 'app-mail',
  templateUrl: './mail.component.html',
  styleUrls: ['./mail.component.css']
})
export class MailComponent implements OnInit, AfterViewInit{
  @ViewChild(MatPaginator) paginator: MatPaginator;
  columnas:string[];
  listUsuarios: UsuarioModel[];
  mailForm: FormGroup;
  mailRequest: MailRequest = new MailRequest();
  dataSource:MatTableDataSource<UsuarioModel> ;
  constructor(private _formBuilder: FormBuilder,
              private _mailService: MailService,
              private _snackBar: MatSnackBar,
              private _userService: UsuarioService) {
                this.columnas = ['id', 'nombre', 'apellido', 'email', 'dni','accion']
               }

  ngOnInit(): void {
    this.mailForm = this._formBuilder.group({
      email:['', Validators.required],
      asunto:['', Validators.required],
      mensaje:['', Validators.required]
    })
    this._userService.getUsuarios().subscribe(resp => {
      
      this.listUsuarios = resp.data;
      this.dataSource =  new MatTableDataSource<UsuarioModel>(this.listUsuarios)
      this.dataSource.paginator = this.paginator;
    })
  }
  ngAfterViewInit() {
    
    
  }
enviarMail(){

this.mailRequest.body = this.mailForm.get('mensaje').value;
this.mailRequest.subject = this.mailForm.get('asunto').value;
this.mailRequest.toEmail = this.mailForm.get('email').value;



this._mailService.registrarMail(this.mailRequest).subscribe(resp =>{
  
  if(resp.exito === 1){
    this._snackBar.open('Email enviado con éxito!','',{
      duration: 2000,
      verticalPosition:'top'
    });
  }else{
    this._snackBar.open('Email NO enviado','',{
      duration: 2000
    });
  }
});
}
seleccionarUsuario(emailUser: string){
  this.mailForm.get('email').setValue(emailUser);
}
applyFilter(event: Event) {
  console.log(event)
  const filterValue = (event.target as HTMLInputElement).value;
  console.log(filterValue)
  this.dataSource.filter = filterValue.trim().toLowerCase();

 if (this.dataSource.paginator) {
    this.dataSource.paginator.firstPage();
  }
}
}
