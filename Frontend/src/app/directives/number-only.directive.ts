import { Directive, HostListener } from '@angular/core';

@Directive({
  selector: '[appNumberOnly]',
  standalone: true // Ensure the directive is standalone for direct imports
})
export class NumberOnlyDirective {
  @HostListener('keydown', ['$event'])
  onKeyDown(event: KeyboardEvent): void {
    const allowedKeys = ['Backspace', 'Tab', 'ArrowLeft', 'ArrowRight', 'Delete', 'Enter', '.'];
    const isNumberKey = event.key >= '0' && event.key <= '9';
    const isModifierKey = event.ctrlKey || event.metaKey || event.altKey;
    const input = event.target as HTMLInputElement;
    const inputValue = input?.value;

    // Prevent negative sign
    if (event.key === '-') {
      event.preventDefault();
    }

    // Prevent multiple dots
    if (event.key === '.' && inputValue.includes('.')) {
      event.preventDefault();
    }

    // Prevent more than 3 numbers in the decimal part
    if (inputValue.includes('.')) {
      const decimalPart = inputValue.split('.')[1];
      if (decimalPart?.length >= 3 && isNumberKey) {
        event.preventDefault();
      }
    }

    // Simulate the value after key press
    let nextValue = inputValue;
    if (isNumberKey || event.key === '.') {
      const selectionStart = input.selectionStart ?? inputValue.length;
      const selectionEnd = input.selectionEnd ?? inputValue.length;
      nextValue = inputValue.substring(0, selectionStart) + event.key + inputValue.substring(selectionEnd);
    }

    // Prevent value < 0 or > 999999999
    if (nextValue && !isNaN(Number(nextValue))) {
      const num = Number(nextValue);
      if (num < 0 || num > 999999999) {
        event.preventDefault();
      }
    }

    if (!isNumberKey && !allowedKeys.includes(event.key) && !isModifierKey) {
      event.preventDefault();
    }
  }

  @HostListener('paste', ['$event'])
  onPaste(event: ClipboardEvent): void {
    const clipboardData = event.clipboardData?.getData('text') || '';
    if (!/^(\d+)(\.\d{1,3})?$/.test(clipboardData)) {
      event.preventDefault();
      return;
    }
    const num = Number(clipboardData);
    if (isNaN(num) || num < 0 || num > 999999999) {
      event.preventDefault();
    }
  }

  @HostListener('input', ['$event'])
  onInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    let value = input.value.replace(/,/g, '');
    // Remove all commas for processing
    if (value && !isNaN(Number(value))) {
      let num = Number(value);
      if (num > 999999999) {
        num = 999999999;
        value = num.toString();
      }
      // Format as currency with commas
      const parts = value.split('.');
      parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
      input.value = parts.join('.');
    }
  }
}
