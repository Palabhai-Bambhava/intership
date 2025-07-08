import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-studentform',
  standalone: true, // This is key for standalone components
  imports: [CommonModule, FormsModule], // <-- ADD THIS
  templateUrl: './studentform.component.html',
  styleUrls: ['./studentform.component.css']
})
export class StudentformComponent {
  studentName: string = '';
  studentMail: string = '';
  submitted: boolean = false;

  students: { name: string; email: string }[] = [];

  submitForm() {
    if (this.studentName && this.studentMail) {
      this.students.push({
        name: this.studentName,
        email: this.studentMail
      });
      this.submitted = true;
      this.studentName = '';
      this.studentMail = '';
    }
  }
}
