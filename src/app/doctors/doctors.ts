
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

interface Doctor {
  name: string;
  specialization: string;
  experience: number | null;
  phone: string;
}

@Component({
  selector: 'app-doctors',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './doctors.html',
  styleUrl: './doctors.css',
})
export class Doctors {
  showForm = false;
  editingIndex: number | null = null;

  doctor: Doctor = {
    name: '',
    specialization: '',
    experience: null,
    phone: ''
  };

  doctors: Doctor[] = [];

  addDoctor(): void {
    if (
      !this.doctor.name.trim() ||
      !this.doctor.specialization ||
      !this.doctor.experience ||
      !this.doctor.phone.trim()
    ) {
      return;
    }

    if (this.editingIndex !== null) {
      this.doctors[this.editingIndex] = { ...this.doctor };
    } else {
      this.doctors.push({ ...this.doctor });
    }

    this.resetForm();
  }

  editDoctor(index: number): void {
    this.editingIndex = index;
    this.doctor = { ...this.doctors[index] };
    this.showForm = true;
  }

  deleteDoctor(index: number): void {
    if (confirm('Are you sure you want to delete this doctor?')) {
      this.doctors.splice(index, 1);
    }
  }

  resetForm(): void {
    this.doctor = {
      name: '',
      specialization: '',
      experience: null,
      phone: ''
    };

    this.editingIndex = null;
    this.showForm = false;
  }
}
