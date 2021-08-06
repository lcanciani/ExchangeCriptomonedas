import { BancoCbu } from "./bancoCbu.model";

export class RegistrarUsuarioModel{
    nombre: string;
    apellido: string;
    direccion: string;
    email: string;
    password: string;
    dni: string;
    bancoCbu: BancoCbu[];
}