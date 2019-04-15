#include <stdio.h>
#include<time.h>
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
	clock_t s = clock();
	for(int b = 0; b < 5000000; ++b)
	{
		int a[] = { 7,6,2,1,3,8,12,32,66,22,34,11,25,109,30,9,9,47 };
		long size = 18;
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
	}
	printf("ExTime -> %.2fsec", (double)(clock() - s)/CLOCKS_PER_SEC);
	return 0;
}


