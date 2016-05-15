# TableDetector
TableDetector is a C# console application which is used to train and evaluate files of specific format.
It internally uses python scrip to build statistical model and to predict result using the built model

#Usage
Use the following commands training file and predicting results.
	TablePredictor.exe train data.csv train.txt
	TablePredictor.exe predict blindset_table_out.csv eval.txt predicted_output.txt
	
#Prerequisites:
	HTMLAgilityPack
	Iron Python
	NLTK

Note:
	If you change the default install location of python libraries update it accordingly in 'Predictor.cs' file.
