import { Component, inject } from '@angular/core';
import { HttpService } from '../../services/http.service';
import { IEmployee } from '../../types/employee';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { IDepartment } from '../../types/departments';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { TableComponent } from '../../components/table/table.component';
import {
    MatDialog,
    MatDialogActions,
    MatDialogClose,
    MatDialogContent,
    MatDialogRef,
    MatDialogTitle,
  } from '@angular/material/dialog';
import { EmployeeFormComponent } from './employee-form/employee-form.component';

@Component({
  selector: 'app-employee',
  imports: [TableComponent,MatButtonModule, MatFormFieldModule, MatSelectModule, MatInputModule, FormsModule, MatDatepickerModule, MatNativeDateModule],
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.scss'
})
export class EmployeeComponent {
    httpService = inject(HttpService);
    employeeList: IEmployee[] = [];
    showCols = ['id','name','email','phone','action',]
    
    ngOnInit() {
        this.getLatestDate();
    }
    getLatestDate(){
        this.httpService.getEmployeesList().subscribe((result)=> {
            this.employeeList = result;
        })
    }
    edit(employee:IEmployee){
        let ref = this.dialog.open(EmployeeFormComponent, {
            panelClass:'m-auto',
            data: {
                employeeId: employee.id,
            }
        });
        ref.afterClosed().subscribe(result=>{
            this.getLatestDate();
        })
    }
    delete(employee: IEmployee){
        this.httpService.deleteEmployee(employee.id).subscribe(()=>{
            console.log("Funcionario Excluido")
            alert("Funcionario Deletado")
            this.getLatestDate();
        })
    }

    add(){
        this.openDialog();
    }

    readonly dialog = inject(MatDialog);
    openDialog(): void {
        let ref = this.dialog.open(EmployeeFormComponent, {
        panelClass:'m-auto'
        });
        ref.afterClosed().subscribe(result=>{
            this.getLatestDate();
        })
    }  
}
