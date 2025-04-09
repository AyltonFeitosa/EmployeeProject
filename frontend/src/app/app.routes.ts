import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { DepartmentsComponent } from './pages/departments/departments.component';
import { EmployeeComponent } from './pages/employee/employee.component';
import { LoginComponent } from './pages/login/login.component';
import { EmployeedashboardComponent } from './pages/employeedashboard/employeedashboard.component';

export const routes: Routes = [
    {
        path:"",
        component:HomeComponent
    },
    {
        path:"employee-dashboard",
        component:EmployeedashboardComponent
    },
    {
        path:"departments",
        component: DepartmentsComponent
    },
    {
        path:"employees",
        component: EmployeeComponent
    },
    {
        path:"login",
        component: LoginComponent
    }
];
