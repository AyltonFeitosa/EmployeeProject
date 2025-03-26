import { Component, inject } from '@angular/core';
import { HttpService } from '../../services/http.service';
import { IDepartment } from '../../types/departments';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-departments',
  imports: [MatButtonModule, MatFormFieldModule, MatSelectModule, MatInputModule, FormsModule],
  templateUrl: './departments.component.html',
  styleUrl: './departments.component.scss'
})
export class DepartmentsComponent {
  
  httpService = inject(HttpService);

  departments: IDepartment[] = [];

  isFormOpen = false;
  name!:string;
  id!:number;

  ngOnInit(){
    this.getLatestData();    
  }
  getLatestData(){
    this.httpService.getDepartments().subscribe(result => {
      this.departments = result;
    })
  }

  addDepartment() {
    console.log(this.name)
    this.httpService.addDepartment(this.name).subscribe(()=>{
      alert("Departamento Adicionado.");    
      this.isFormOpen = false;    
      this.getLatestData();
    })
  }
  isEdit = false;
  editDepartment(department: IDepartment){
    console.log(this.name=department.name)
    this.name = department.name;
    this.id = department.id;
    this.isFormOpen = true;
    this.isEdit = true;
  }
  updateDepartment(){
    this.httpService.updateDepartment(this.id,this.name).subscribe(()=>{
      alert("Departamento Atualizada");
      this.isFormOpen = false;
      this.isEdit = false;
      this.getLatestData();
    })
  }
  deleteDepartment(id:number){
    this.httpService.deleteDepartment(id).subscribe(()=>{
      alert("Departament excluido.");
      this.getLatestData();
    })
  }


}
