
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

interface Patient {
  name: string;
  age: number | null;
  gender: string;
  phone: string;
}

@Component({
  selector: 'app-patients',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './patients.html',
  styleUrl: './patients.css',
})
export class Patients {
  showForm = false;
  editingIndex: number | null = null;

  patient: Patient = {
    name: '',
    age: null,
    gender: '',
    phone: ''
  };

  patients: Patient[] = [];

  addPatient(): void {
    if (
      !this.patient.name.trim() ||
      !this.patient.age ||
      !this.patient.gender ||
      !this.patient.phone.trim()
    ) {
      return;
    }

    if (this.editingIndex !== null) {
      this.patients[this.editingIndex] = { ...this.patient };
    } else {
      this.patients.push({ ...this.patient });
    }

    this.resetForm();
  }

  editPatient(index: number): void {
    this.editingIndex = index;
    this.patient = { ...this.patients[index] };
    this.showForm = true;
  }

  deletePatient(index: number): void {
    if (confirm('Are you sure you want to delete this patient?')) {
      this.patients.splice(index, 1);
    }
  }

  resetForm(): void {
    this.patient = {
      name: '',
      age: null,
      gender: '',
      phone: ''
    };

    this.editingIndex = null;
    this.showForm = false;
  }
}
