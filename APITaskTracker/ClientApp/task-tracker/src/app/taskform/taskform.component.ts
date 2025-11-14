import { ChangeDetectorRef, Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskItemStatus, TaskPriority, TaskItem } from '../models/task.Models';
import { TaskService } from '../services/task.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-taskform',
  standalone: true,
    imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './taskform.component.html',
  styleUrl: './taskform.component.css'
})
export class TaskformComponent {

  
  form!: FormGroup;
  isEdit = false;
  loading = false;
  error: string | null = null;
  TaskItemStatus = TaskItemStatus;
  TaskPriority = TaskPriority;

  statusKeys = Object.keys(TaskItemStatus).filter(key => isNaN(Number(key))) as (keyof typeof TaskItemStatus)[];
  priorityKeys = Object.keys(TaskPriority).filter(key => isNaN(Number(key))) as (keyof typeof TaskPriority)[];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    public router: Router,
    private taskService: TaskService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      id: [0],
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: [''],
      status: [TaskItemStatus.New, Validators.required],
      priority: [TaskPriority.Low, Validators.required],
      dueDate: [null],
      createdAt: [new Date]
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEdit = true;
      this.loading = true;
      this.taskService.getById(+id).subscribe({
        next: (data: { [x: string]: any; dueDate?: any; }) => {
          data.dueDate = data.dueDate?.toString().split('T')[0];
          this.form.patchValue(data);
          this.loading = false;
          this.cdr.detectChanges();
        },
        error: (err: { message: string | null; }) => {
          this.error = err.message;
          this.loading = false;
        }
      });
    }
  }

  get f() {
    return this.form.controls;
  }

  save() {

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;
    const task: TaskItem = this.form.value;
    const request$ = this.isEdit
      ? this.taskService.update(task.id!, task)
      : this.taskService.create(task);

    request$.subscribe({
      next: () => this.router.navigate(['/']),
      error: (err: { message: string | null; }) => {
        this.error = err.message;
        this.loading = false;
      }
    });
  }

}
