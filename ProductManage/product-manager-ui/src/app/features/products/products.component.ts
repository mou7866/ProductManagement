import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Product } from '../models/product.model';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
  imports: [CommonModule, ReactiveFormsModule]
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  categories: { id: string; name: string }[] = [];
  loading: boolean = true;
  showForm: boolean = false;
  form: FormGroup;
  isEditMode: boolean = false;
  currentProductId: string | null = null;
  private readonly apiUrl = `${environment.apiUrl}/products`;
  private readonly categoriesUrl = `${environment.apiUrl}/categories`;

  constructor(private http: HttpClient, private fb: FormBuilder) {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      price: [0, [Validators.required, Validators.min(0)]],
      categoryId: ['', Validators.required], // categoryId is required
      status: ['', Validators.required],
      stockQuantity: [0, [Validators.required, Validators.min(0)]],
      imageUrl: ['']
    });
  }

  ngOnInit(): void {
    this.fetchProducts();
    this.fetchCategories();
  }

  fetchProducts(): void {
    this.loading = true;
    const params = {
      page: 1,
      pageSize: 100,
      sortOrder: 'asc'
    };

    this.http.get<{ products: Product[] }>(this.apiUrl, { params }).subscribe({
      next: (data) => {
        this.products = data.products;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching products:', err);
        this.loading = false;
      }
    });
  }

  fetchCategories(): void {
    this.http.get<{ id: string; name: string }[]>(this.categoriesUrl).subscribe({
      next: (data) => {
        this.categories = data;
      },
      error: (err) => {
        console.error('Error fetching categories:', err);
      }
    });
  }

  addProduct(): void {
    this.isEditMode = false;
    this.showForm = true;
    this.form.reset({
      name: '',
      description: '',
      price: 0,
      categoryId: '', // Reset categoryId
      status: '',
      stockQuantity: 0,
      imageUrl: ''
    });
  }

  editProduct(product: Product): void {
    this.isEditMode = true;
    this.showForm = true;
    this.currentProductId = product.id;
    this.form.setValue({
      name: product.name,
      description: product.description,
      price: product.price,
      categoryId: product.categoryId,
      categoryName: product.categoryName, // Set categoryId correctly
      status: product.status,
      stockQuantity: product.stockQuantity,
      imageUrl: product.imageUrl || ''
    });
  }

  saveProduct(): void {
    if (this.form.invalid) return;

    const productData = this.form.getRawValue();
    if (this.isEditMode && this.currentProductId) {
      this.updateProduct(this.currentProductId, productData);
    } else {
      this.createProduct(productData);
    }
  }

  private createProduct(product: Product): void {
    this.http.post<Product>(this.apiUrl, product).subscribe({
      next: () => {
        this.fetchProducts();
        this.showForm = false;
      },
      error: (err) => {
        if (err.status === 400 && err.error.errors) {
          this.displayValidationErrors(err.error.errors);
        } else {
          console.error('Error adding product:', err);
        }
      }
    });
  }

  private updateProduct(productId: string, product: Product): void {
    this.http.put(`${this.apiUrl}/${productId}`, product).subscribe({
      next: () => {
        this.fetchProducts();
        this.showForm = false;
      },
      error: (err) => {
        if (err.status === 400 && err.error.errors) {
          this.displayValidationErrors(err.error.errors);
        } else {
          console.error('Error updating product:', err);
        }
      }
    });
  }

  private displayValidationErrors(errors: { propertyName: string; errorMessage: string }[]): void {
    errors.forEach((error) => {
      const control = this.form.get(this.mapPropertyNameToFormControl(error.propertyName));
      if (control) {
        control.setErrors({ serverError: error.errorMessage });
      }
    });
  }

  private mapPropertyNameToFormControl(propertyName: string): string {
    return propertyName.charAt(0).toLowerCase() + propertyName.slice(1);
  }

  deleteProduct(productId: string): void {
    if (!confirm('Are you sure you want to delete this product?')) return;

    this.http.delete(`${this.apiUrl}/${productId}`).subscribe({
      next: () => {
        this.fetchProducts();
      },
      error: (err) => {
        console.error('Error deleting product:', err);
      }
    });
  }

  cancel(): void {
    this.showForm = false;
    this.form.reset();
  }
}