import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {

  employees: any[] = [];

  empModel = {
    id: 0,
    name: '',
    department: '',
    salary: 0
  };

  isEditMode = false;

  constructor(
    private empService: EmployeeService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.loadEmployees();
  }

  loadEmployees() {
    this.empService.getEmployees().subscribe(res => {
      console.log("Loaded employees:", res);

      this.employees = [...res]; 

      this.cdr.markForCheck();   
    });
  }

  addEmployee() {
    this.empService.addEmployee(this.empModel).subscribe(() => {
      this.resetForm();
      this.loadEmployees();
      this.cdr.markForCheck();
    });
  }

  editEmployee(emp: any) {
    this.isEditMode = true;
    this.empModel = { ...emp };
    this.cdr.markForCheck();
  }

  updateEmployee() {
    this.empService.updateEmployee(this.empModel.id, this.empModel).subscribe(() => {
      this.resetForm();
      this.loadEmployees();
      this.isEditMode = false;
      this.cdr.markForCheck();
    });
  }

  deleteEmployee(id: number) {
    this.empService.deleteEmployee(id).subscribe(() => {
      this.loadEmployees();
      this.cdr.markForCheck();
    });
  }

  resetForm() {
    this.empModel = {
      id: 0,
      name: '',
      department: '',
      salary: 0
    };
    this.isEditMode = false;
    this.cdr.markForCheck();
  }
  onSubmit(form: any) {
  if (form.invalid) {
    alert("Please fill all required fields!");
    return;
  }

  if (this.isEditMode) {
    this.updateEmployee();
  } else {
    this.addEmployee();
  }
}

}
