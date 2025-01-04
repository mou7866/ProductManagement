import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Category } from '../models/category.model';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['categories.component.css'],
  imports: [CommonModule, ReactiveFormsModule]
})
export class CategoriesComponent implements OnInit {
  categories: Category[] = [];
  loading: boolean = true;
  showForm: boolean = false;
  form: FormGroup;
  isEditMode: boolean = false;
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
    this.http.get<Category[]>(this.apiUrl).subscribe({
      next: (data) => {
        this.categories = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching categories:', err);
        this.loading = false;
      }
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
    this.http.post<Category>(this.apiUrl, category).subscribe({
      next: () => {
        this.fetchCategories();
        this.showForm = false;
      },
      error: (err) => {
        console.error('Error adding category:', err);
      }
    });
  }

  private updateCategory(categoryId: string, category: Category): void {
    this.http.put(`${this.apiUrl}/${categoryId}`, category).subscribe({
      next: () => {
        this.fetchCategories();
        this.showForm = false;
      },
      error: (err) => {
        console.error('Error updating category:', err);
      }
    });
  }

  deleteCategory(categoryId: string): void {
    if (!confirm('Are you sure you want to delete this category?')) return;
  
    this.http.delete(`${this.apiUrl}/${categoryId}`).subscribe({
      next: () => {
        this.fetchCategories();
      },
      error: (err) => {
        if (err.status === 409) {
          alert('This category cannot be deleted because it has associated products.');
        } else {
          console.error('Error deleting category:', err);
        }
      }
    });
  }  

  cancel(): void {
    this.showForm = false;
    this.form.reset();
  }
}
