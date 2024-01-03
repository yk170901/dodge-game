float InitialTime = - 1;

void IterateForAlpha_float(float Time, out float Out)
{
	if(InitialTime == -1)
		InitialTime = Time;

	Out = Time - InitialTime;
}
