import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeService } from '../../services/employee.service';
imports: [FormsModule, CommonModule]


@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit{

  employees: any[] = [];

  // Form model
  empModel = {
    id: 0,
    name: '',
    department: '',
    salary: 0
  };

  // 🔥 THIS FIXES YOUR ERROR
  isEditMode: boolean = false;

  constructor(private empService: EmployeeService) {}

  ngOnInit() {
    this.loadEmployees();
  }

  loadEmployees() {
    this.empService.getEmployees().subscribe((res) => {
      this.employees = res;
    });
  }

  addEmployee() {
    this.empService.addEmployee(this.empModel).subscribe(() => {
      alert('Employee Added');
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
      alert('Employee Updated');
      this.resetForm();
      this.loadEmployees();
      this.isEditMode = false;
    });
  }

  deleteEmployee(id: number) {
    if (confirm('Are you sure?')) {
      this.empService.deleteEmployee(id).subscribe(() => {
        alert('Deleted');
        this.loadEmployees();
      });
    }
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

  downloadExcel() {
  this.empService.downloadExcel().subscribe((file) => {
    const blob = new Blob([file], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = "employees.xlsx";
    a.click();
  });
}

downloadPdf() {
  this.empService.downloadPdf().subscribe((file) => {
    const blob = new Blob([file], { type: 'application/pdf' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = "employees.pdf";
    a.click();
  });
}

}
