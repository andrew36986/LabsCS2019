#include <stdio.h>
int increment(long inc[], long size)
{
	int p1, p2, p3, s;
	p1 = p2 = p3 = 1;
	s = -1;
	do
	{
		if (++s % 2)
		{
			inc[s] = 8 * p1 - 6 * p2 + 1;
		}
		else
		{
			inc[s] = 9 * p1 - 9 * p3 + 1;
			p2 *= 2;
			p3 *= 2;
		}
		p1 *= 2;
	} while (3 * inc[s] < size);

	return s > 0 ? --s : 0;
}
int main()
{
	int a[10000] = {};
	for (int i = 0; i < 10000; i++) { a[i] = rand(); }
	long size = 10000;
	long inc, i, j, seq[40];
	int s;
		
	s = increment(seq, size);
	while (s >= 0)
		{
		inc = seq[s--];
		for (i = inc; i < size; ++i)
		{
			int temp = a[i];
			for (j = i; (j >= inc) && (temp < a[j - inc]); j -= inc) {
				a[j] = a[j - inc];
			}
			a[j] = temp;
		}
	}
	return 0;
}


