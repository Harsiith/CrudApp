import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeService } from '../../services/employee.service';

import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';

import * as ExcelJS from 'exceljs';
import { saveAs } from 'file-saver';

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
      this.employees = [...res];
      this.cdr.markForCheck();
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
    this.empModel = { id: 0, name: '', department: '', salary: 0 };
    this.isEditMode = false;
  }

  onSubmit(form: any) {
    if (form.invalid) {
      alert("Please fill all required fields!");
      return;
    }

    this.isEditMode ? this.updateEmployee() : this.addEmployee();
  }

  // ------------------------------------------------------
  // 🔽 DOWNLOAD AS PDF
  // ------------------------------------------------------
  downloadPDF() {
    const doc = new jsPDF();

    doc.text("Employee List", 14, 10);

    autoTable(doc, {
      startY: 20,
      head: [['ID', 'Name', 'Department', 'Salary']],
      body: this.employees.map(emp => [
        emp.id,
        emp.name,
        emp.department,
        emp.salary
      ])
    });

    doc.save("Employees.pdf");
  }

  // ------------------------------------------------------
  // 🔽 DOWNLOAD AS EXCEL
  // ------------------------------------------------------
  async downloadExcel() {
    const workbook = new ExcelJS.Workbook();
    const worksheet = workbook.addWorksheet('Employees');

    // Add header row
    worksheet.addRow(['ID', 'Name', 'Department', 'Salary']);

    // Add data rows
    this.employees.forEach(emp => {
      worksheet.addRow([emp.id, emp.name, emp.department, emp.salary]);
    });

    // Create file buffer
    const buffer = await workbook.xlsx.writeBuffer();

    // Save file
    saveAs(new Blob([buffer]), 'Employees.xlsx');
  }

}
