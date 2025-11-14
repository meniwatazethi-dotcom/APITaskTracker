import { ChangeDetectorRef, Component } from '@angular/core';
import { TaskService } from '../services/task.service';
import { Router } from '@angular/router';
import { TaskItemStatus, TaskPriority, TaskItem } from '../models/task.Models';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [FormsModule,DatePipe,CommonModule],
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.css'
})
export class TaskListComponent {
TaskItemStatus = TaskItemStatus;
  TaskPriority = TaskPriority;

  getStatusName(value: number): string {
    return TaskItemStatus[value];
  }

  getPriorityName(value: number): string {
    return TaskPriority[value];
  }
  tasks: TaskItem[] = [];
  error: string | null = null;
  isLoading = false;

  searchText = '';
  sortOrder: 'duedate:asc' | 'duedate:desc' = 'duedate:asc';

  constructor(
    private taskService: TaskService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.loadTasks();
  }

  loadTasks() {
    this.isLoading = true;
    this.error = null;

    this.taskService.getAll(this.searchText, this.sortOrder).subscribe({
      next: (data) => {
        debugger
        this.tasks = data;
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = err.message || 'Error loading tasks';
        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });
  }

  sortBy(order: 'duedate:asc' | 'duedate:desc') {
    this.sortOrder = order;
    this.loadTasks();
  }

  deleteTask(id: number) {
    if (!confirm('Delete this task?')) return;
    this.isLoading = true;

    this.taskService.delete(id).subscribe({
      next: () => this.loadTasks(),
      error: (err) => {
        this.error = err.message || 'Error deleting task';
        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });
  }

  newTask() {
    this.router.navigate(['/tasks/add']);
  }

  editTask(id: number) {
    this.router.navigate([`/tasks/edit/${id}`]);
  }
}
