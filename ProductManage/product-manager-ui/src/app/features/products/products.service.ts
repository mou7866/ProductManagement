import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { Product } from '../models/product.model';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private productsSubject = new BehaviorSubject<Product[]>([]);
  private categoriesSubject = new BehaviorSubject<{ id: string; name: string }[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);

  products$ = this.productsSubject.asObservable();
  categories$ = this.categoriesSubject.asObservable();
  loading$ = this.loadingSubject.asObservable();

  private readonly apiUrl = `${environment.apiUrl}/products`;
  private readonly categoriesUrl = `${environment.apiUrl}/categories`;

  constructor(private http: HttpClient) {}

  fetchProducts(): void {
    this.loadingSubject.next(true);
    this.http.get<{ products: Product[] }>(this.apiUrl, { params: { page: 1, pageSize: 100, sortOrder: 'asc' } })
      .pipe(
        catchError((err) => {
          console.error('Error fetching products:', err);
          return [];
        }),
        finalize(() => this.loadingSubject.next(false))
      )
      .subscribe((data) => this.productsSubject.next(data.products));
  }

  fetchCategories(): void {
    this.http.get<{ id: string; name: string }[]>(this.categoriesUrl)
      .subscribe({
        next: (data) => this.categoriesSubject.next(data),
        error: (err) => console.error('Error fetching categories:', err)
      });
  }

  createProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }

  updateProduct(productId: string, product: Product): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${productId}`, product);
  }

  deleteProduct(productId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${productId}`);
  }
}