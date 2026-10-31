
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

interface Appointment {
  patientName: string;
  doctorName: string;
  appointmentDate: string;
  appointmentTime: string;
  status: string;
}

@Component({
  selector: 'app-appointments',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './appointments.html',
  styleUrl: './appointments.css',
})
export class Appointments {
  showForm = false;
  editingIndex: number | null = null;

  appointment: Appointment = {
    patientName: '',
    doctorName: '',
    appointmentDate: '',
    appointmentTime: '',
    status: 'Scheduled'
  };

  appointments: Appointment[] = [];

  addAppointment(): void {
    if (
      !this.appointment.patientName.trim() ||
      !this.appointment.doctorName.trim() ||
      !this.appointment.appointmentDate ||
      !this.appointment.appointmentTime ||
      !this.appointment.status
    ) {
      return;
    }

    if (this.editingIndex !== null) {
      this.appointments[this.editingIndex] = {
        ...this.appointment
      };
    } else {
      this.appointments.push({
        ...this.appointment
      });
    }

    this.resetForm();
  }

  editAppointment(index: number): void {
    this.editingIndex = index;
    this.appointment = {
      ...this.appointments[index]
    };
    this.showForm = true;
  }

  deleteAppointment(index: number): void {
    if (confirm('Are you sure you want to delete this appointment?')) {
      this.appointments.splice(index, 1);
    }
  }

  resetForm(): void {
    this.appointment = {
      patientName: '',
      doctorName: '',
      appointmentDate: '',
      appointmentTime: '',
      status: 'Scheduled'
    };

    this.editingIndex = null;
    this.showForm = false;
  }
}
