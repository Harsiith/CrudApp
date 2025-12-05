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
    private cdr: ChangeDetectorRef   // 🔥 inject ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.loadEmployees();
  }

  loadEmployees() {
    this.empService.getEmployees().subscribe(res => {
      console.log("Loaded employees:", res);

      this.employees = [...res];   // 🔥 spread operator ensures new reference

      this.cdr.detectChanges();    // 🔥 FORCES Angular to re-render immediately
    });
  }

  addEmployee() {
    this.empService.addEmployee(this.empModel).subscribe(() => {
      this.resetForm();
      this.loadEmployees();
    });
  }

  editEmployee(emp: any) {
    this.isEditMode = true;
    this.empModel = { ...emp };
  }

  updateEmployee() {
    this.empService.updateEmployee(this.empModel.id, this.empModel).subscribe(() => {
      this.resetForm();
      this.loadEmployees();
      this.isEditMode = false;
    });
  }

  deleteEmployee(id: number) {
    this.empService.deleteEmployee(id).subscribe(() => {
      this.loadEmployees();
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
  }
}
