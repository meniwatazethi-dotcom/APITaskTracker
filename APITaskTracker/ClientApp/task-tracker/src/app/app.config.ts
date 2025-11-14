import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, Routes } from '@angular/router';
import { TaskformComponent } from './taskform/taskform.component';
import { TaskListComponent } from './task-list/task-list.component';


const routes: Routes = [
  { path: '', redirectTo: 'tasks', pathMatch: 'full' },
  { path: 'tasks', component: TaskListComponent },
  { path: 'tasks/add', component: TaskformComponent },
  { path: 'tasks/edit/:id', component: TaskformComponent },
  { path: '**', redirectTo: 'tasks' }
];

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes)]
};





// import {
//   ApplicationConfig,
//   provideBrowserGlobalErrorListeners,
//   provideZonelessChangeDetection,
// } from '@angular/core';
// import { provideRouter, Routes } from '@angular/router';
// import { provideClientHydration, withEventReplay } from '@angular/platform-browser';
// import { provideHttpClient } from '@angular/common/http';
// import { TaskList } from './task-list/task-list';
// import { TaskForm } from './task-form/task-form';

// const routes: Routes = [
//   { path: '', redirectTo: 'tasks', pathMatch: 'full' },
//   { path: 'tasks', component: TaskList },
//   { path: 'tasks/add', component: TaskForm },
//   { path: 'tasks/edit/:id', component: TaskForm },
//   { path: '**', redirectTo: 'tasks' }
// ];

// export const appConfig: ApplicationConfig = {
//   providers: [
//     provideBrowserGlobalErrorListeners(),
//     provideZonelessChangeDetection(),
//     provideRouter(routes),
//     provideClientHydration(withEventReplay()),
//     provideHttpClient()
//   ]
// };
