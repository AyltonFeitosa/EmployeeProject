import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IDepartment } from '../types/departments';
import { IEmployee } from '../types/employee';
import { EmployeeComponent } from '../pages/employee/employee.component';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  http = inject(HttpClient)
  constructor() { }

  getDepartments(){
    return this.http.get<IDepartment[]>(environment.apiUrl+"/api/Departament");
  }
  addDepartment(name:string){
    return this.http.post(environment.apiUrl+"/api/Departament",{
      name:name
    });
  }
  updateDepartment(id:number,name:string){
    return this.http.put(environment.apiUrl+"/api/Departament/"+id,{
      name: name,
    });
  }
  deleteDepartment(id:number){
    return this.http.delete(environment.apiUrl+"/api/Departament/"+id)
  }

  getEmployeesList(){
    return this.http.get<IEmployee[]>(environment.apiUrl+"/api/employee")
  }
  
  addEmployee(employee: IEmployee){
    return this.http.post(environment.apiUrl+"/api/employee", employee)
  }
  getEmployeeById(id:number){
    return this.http.get<IEmployee>(environment.apiUrl+"/api/employee/"+id)    
  }
  updateEmployee(id:number, employee: IEmployee){
    return this.http.put(environment.apiUrl+"/api/employee/"+id,employee)    
  }
  deleteEmployee(id:number){
    return this.http.delete(environment.apiUrl+"/api/employee/"+id)
  }
}
