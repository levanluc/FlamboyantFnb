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
    const inputValue = (event.target as HTMLInputElement)?.value;

    if (event.key === '.' && inputValue.includes('.')) {
      event.preventDefault(); // Prevent multiple dots
    }

    if (inputValue.includes('.')) {
      const decimalPart = inputValue.split('.')[1];
      if (decimalPart?.length >= 3 && isNumberKey) {
        event.preventDefault(); // Prevent more than 3 numbers in the decimal part
      }
    }

    if (!isNumberKey && !allowedKeys.includes(event.key) && !isModifierKey) {
      event.preventDefault();
    }
  }

  @HostListener('paste', ['$event'])
  onPaste(event: ClipboardEvent): void {
    const clipboardData = event.clipboardData?.getData('text') || '';
    if (!/^\d+(\.\d{1,3})?$/.test(clipboardData)) { // Validate single decimal format with up to 3 numbers
      event.preventDefault();
    }
  }
}
