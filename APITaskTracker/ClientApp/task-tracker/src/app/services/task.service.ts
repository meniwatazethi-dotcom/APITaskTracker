import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { environment } from '../../environment/environment';
import { TaskItem } from '../models/task.Models';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

   private baseUrl = `${environment.apiUrl}/tasks`;

  constructor(private http: HttpClient) { }

  getAll(description?: string, dueDate?: string): Observable<TaskItem[]> {
    let params = new HttpParams();
    if (description) params = params.set('description', description);
    if (dueDate) params = params.set('dueDate', dueDate);

    return this.http.get<TaskItem[]>(this.baseUrl, { params })
      .pipe(catchError(this.handleError));
  }

  getById(id: number) {
    return this.http.get<TaskItem>(`${this.baseUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  create(task: TaskItem) {
    return this.http.post<TaskItem>(this.baseUrl, task)
      .pipe(catchError(this.handleError));
  }

  update(id: number, task: TaskItem) {
    return this.http.put<TaskItem>(`${this.baseUrl}/${id}`, task)
      .pipe(catchError(this.handleError));
  }

  delete(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    const msg = error.error?.detail || error.error?.message || 'Unexpected error occurred contact support team.';
    return throwError(() => new Error(msg));
  }
}
