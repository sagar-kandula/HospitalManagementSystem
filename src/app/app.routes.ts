
import { Routes } from '@angular/router';

import { Login } from './login/login';
import { Dashboard } from './dashboard/dashboard';
import { Patients } from './patients/patients';
import { Doctors } from './doctors/doctors';
import { Appointments } from './appointments/appointments';

export const routes: Routes = [
  { path: 'login', component: Login },
  { path: 'dashboard', component: Dashboard },
  { path: 'patients', component: Patients },
  { path: 'doctors', component: Doctors },
  { path: 'appointments', component: Appointments },

  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' }
];
