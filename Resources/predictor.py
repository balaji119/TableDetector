import nltk
import sys
import pickle

mode_name = "nb.model"

class trainer:
    def generate_feature(self, text):
        features = {}
        for token in text:
            features[token] = 1
        return features

    def learn(self, features, path):
        featuresets = []
        for feature, tag in features:
            featuresets.append((self.generate_feature(feature), tag))
        classifier = nltk.NaiveBayesClassifier.train(featuresets)
        pickle.dump( classifier, open( mode_name, "wb" ) )

class evaluator:
    def __init__(self):
        self._evaluator = pickle.load( open( mode_name, "rb" ) )

    def generate_feature(self, text):
        features = {}
        for token in text:
            features[token] = 1
        return features

    def evaluate(self, features):
        return self._evaluator.classify(self.generate_feature(features))
        
def learn(path):
    features = []
    reader = open(path, "r")
    lines = reader.readlines()
    for line in lines:
        tokens = line.split('\t')
        features.append((tokens[:-1], tokens[-1]))
    p = trainer()
    return p.learn(features, path)

def evaluate(path, predPath):
    result = []
    e = evaluator()
    reader = open(path, "r")
    lines = reader.readlines();
    for line in lines:
        tokens = line.split('\t')
        result.append(e.evaluate(tokens[:-1]))
    writer = open(predPath, "w")
    writer.writelines(result)
    
