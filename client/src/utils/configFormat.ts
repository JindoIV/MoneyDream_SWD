export function formatMoney(number: any) {
  return Number(number).toLocaleString('vi-VN');
}

export function formatQuantity(number: number) {
  if (number < 1) return 1;
  if (number > 99) return 99;
  return number;
}
