import { Component} from '@angular/core';
import{CriptomonedaModel} from './criptomoneda.model';

@Component({
  selector: 'app-criptomonedas',
  templateUrl: './criptomonedas.component.html',
  styleUrls: ['./criptomonedas.component.css']
})
export class CriptomonedasComponent  {

  listaCripto : CriptomonedaModel[];

  actualizarLista(e){
    this.listaCripto = e;
  }

 /*  listCriptomonedas : CriptomonedaModel[] ;
  public columnas : string[] = ['id','nombre','accion'];
  criptomonedaModel : CriptomonedaModel
  public criptomonedasForm: FormGroup;
  constructor(private criptomonedasService: CriptomonedasService,
              private formBuilder: FormBuilder,
              private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.criptomonedasForm = this.formBuilder.group({
      idCriptomoneda:[5, Validators.required], 
      nombre:[''], 
      precioCompra:[''],
     stockDisponible:[''], 
      simbolo:[''], 
     capitalizacion :[''],
     stockTotal:[''],
      precioVenta :[''],
    imagenUrl: ['']
    });
    this.criptomonedasService.getCriptomonedas().subscribe(response => {
      this.listCriptomonedas = response;
      console.log(response);
    })
  }
  addCriptomoneda(){

    console.log("llego al metodo add de la clase criptomonedas component");
    this.criptomonedasService.addCriptomoneda(this.criptomonedasForm.value).subscribe(Response => {
      this.snackBar.open('Criptomoneda insertada con éxito','',{
        duration: 2000
      });
    });
  }
  editarCriptomoneda(){
    this.criptomonedasService.getCriptomonedaById(this.criptomonedasForm.get('nombre').value).subscribe(model =>{
      this.criptomonedaModel = model.data;
    })
    this.criptomonedasForm.patchValue({
      
    })
  } */




}
