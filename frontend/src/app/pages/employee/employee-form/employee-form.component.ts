import { Component, inject, Input } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import {MatRadioModule} from '@angular/material/radio';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatIconModule} from '@angular/material/icon';
import { CommonModule } from '@angular/common'; 
import { IDepartment } from '../../../types/departments';
import { HttpService } from '../../../services/http.service';
import { MAT_DIALOG_DATA, MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';



@Component({
  selector: 'app-employee-form',
  imports: [CommonModule, MatInputModule, FormsModule, ReactiveFormsModule, MatSelectModule, MatCardModule, MatButtonModule, MatRadioModule,MatDatepickerModule, MatIconModule,],
  templateUrl: './employee-form.component.html',
  styleUrl: './employee-form.component.scss'
})
export class EmployeeFormComponent {
  fb = inject(FormBuilder);
  @Input() employeeId!: number;
  employeeForm = this.fb.group({
    id: [0],
    name: ['', [Validators.required, Validators.minLength(3)]],
    email: ['', [Validators.required, Validators.email]],
    phone: ['', [Validators.required, Validators.pattern(/^\d{10}/)]],
    gender: [1, Validators.required],
    departmentId: ['', Validators.required],
    jobTitle: ['', [Validators.required]], 
    joiningDate: [null, [Validators.required]],
    lastWorkingDate: [null],
    dateOfBirth: [null, [Validators.required]],
  });

  departments:IDepartment[]=[];
  httpService=inject(HttpService);

  ngOnInit(){
    this.httpService.getDepartments().subscribe(result=>{
      this.departments = result;
    });
    if (this.data && this.data.employeeId){
      this.httpService.getEmployeeById(this.data.employeeId).subscribe((result) => {
        console.log(result);
        this.employeeForm.patchValue(result as any);
        this.employeeForm.get('gender')?.disable();
        this.employeeForm.get('joiningDate')?.disable();
        this.employeeForm.get('dateOfBirth')?.disable();
      });
    } else{
      console.log('Criando novo funcionário');
    }
  }
  dialogRef = inject(MatDialogRef<EmployeeFormComponent>);
  data = inject(MAT_DIALOG_DATA) ?? {};
  onSubmit(){
    let value :any = this.employeeForm.value;
    
    if (!value.lastWorkingDate) {
      value.lastWorkingDate = null;
    }
    if(this.data.employeeId){
      
      this.httpService.updateEmployee(this.data.employeeId, value).subscribe(()=>{
        alert("Funcionario atualizado")
        this.dialogRef.close()
      })
    }else{    
      console.log("valid", this.employeeForm.valid)
      let value :any = this.employeeForm.value;
      this.httpService.addEmployee(value).subscribe(()=>{
        alert("Funcionario adicionado")
        this.dialogRef.close()
      })
    }
  }
}

