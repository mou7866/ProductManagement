import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Category } from '../models/category.model';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { environment } from '../../../environments/environment';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
  imports: [CommonModule, ReactiveFormsModule]
})
export class CategoriesComponent implements OnInit {
  categories: Category[] = [];
  loading = false;
  showForm = false;
  form: FormGroup;
  isEditMode = false;
  currentCategoryId: string | null = null;
  private readonly apiUrl = `${environment.apiUrl}/categories`;

  constructor(private http: HttpClient, private fb: FormBuilder) {
    this.form = this.fb.nonNullable.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      description: ['', [Validators.maxLength(200)]],
      status: ['Active', Validators.required]
    });
  }

  ngOnInit(): void {
    this.fetchCategories();
  }

  fetchCategories(): void {
    this.loading = true;
    this.http.get<Category[]>(this.apiUrl).pipe(
      catchError((err) => {
        console.error('Error fetching categories:', err);
        alert('Failed to load categories. Please try again later.');
        this.loading = false;
        return of([]);
      })
    ).subscribe((data) => {
      this.categories = data;
      this.loading = false;
    });
  }

  addCategory(): void {
    this.isEditMode = false;
    this.showForm = true;
    this.form.reset();
  }

  editCategory(category: Category): void {
    this.isEditMode = true;
    this.showForm = true;
    this.currentCategoryId = category.id;
    this.form.setValue({
      name: category.name,
      description: category.description || '',
      status: category.status
    });
  }

  saveCategory(): void {
    if (this.form.invalid) return;

    const categoryData = this.form.getRawValue();
    if (this.isEditMode && this.currentCategoryId) {
      this.updateCategory(this.currentCategoryId, categoryData);
    } else {
      this.createCategory(categoryData);
    }
  }

  private createCategory(category: Category): void {
    this.http.post<Category>(this.apiUrl, category).pipe(
      catchError((err) => {
        console.error('Error adding category:', err);
        alert('Failed to add category. Please try again.');
        return of(null);
      })
    ).subscribe((result) => {
      if (result) {
        this.fetchCategories();
        this.showForm = false;
      }
    });
  }

  private updateCategory(categoryId: string, category: Category): void {
    this.http.put(`${this.apiUrl}/${categoryId}`, category).pipe(
      catchError((err) => {
        console.error('Error updating category:', err);
        alert('Failed to update category. Please try again.');
        return of(null);
      })
    ).subscribe((result) => {
      if (result) {
        this.fetchCategories();
        this.showForm = false;
      }
    });
  }

  deleteCategory(categoryId: string): void {
    if (!confirm('Are you sure you want to delete this category?')) return;

    this.http.delete(`${this.apiUrl}/${categoryId}`).pipe(
      catchError((err) => {
        if (err.status === 409) {
          alert('This category cannot be deleted because it has associated products.');
        } else {
          console.error('Error deleting category:', err);
          alert('Failed to delete category. Please try again.');
        }
        return of(null);
      })
    ).subscribe((result) => {
      if (result !== null) {
        this.fetchCategories();
      }
    });
  }

  cancel(): void {
    this.showForm = false;
    this.form.reset();
  }
}