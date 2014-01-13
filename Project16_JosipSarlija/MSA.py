from Hypercube import Hypercube
from InputOutput import InputData

class NucleotideScoring:
    MATCH = 1
    MISMATCH = -1
    GAP = -2


class MSA:
    def __init__(self, hypercube):
        assert isinstance(hypercube, Hypercube)
        self.hypercube = hypercube
        self.traceback_path = tuple()
        self.output = list()
        return

    def align(self):
        print ("INFO: starting MSA align.")
        for key in self.hypercube.sorted_keys:
            current_segment = self.hypercube.data[key]
            result = self.calculate_m(current_segment)
            if result is not None:
                current_segment.set_distance(result[0])
                current_segment.set_backtrack_index(result[1])
            else:
                current_segment.set_distance(0)
        print ("INFO: MSA align complete.")
        print ("INFO: Generating traceback path.")
        self.traceback_path = self.generate_traceback_path()
        print ("INFO: Generating output.")
        self.output = self.generate_output(self.traceback_path)
        return

    def calculate_m(self, current_segment):
        results = dict()
        for neighbor in self.hypercube.backward_neighbors(current_segment):
            # calculate M + S for each neighbor
            M = neighbor.get_distance()
            indices_changed = Hypercube.compare_indices(current_segment.index, neighbor.index)
            sequence = list()
            for i in range(0, len(indices_changed)):
                if indices_changed[i]:
                    sequence.append(self.hypercube.sequences[i][current_segment.index[i]])
                else:
                    sequence.append('-')
            result = (M + self.calculate_s(sequence), neighbor.index)
            if result[0] not in results:
                results[result[0]] = list()
                results[result[0]].append(result[1])
            else:
                results[result[0]].append(result[1])

        if len(results) <= 0:
            return None

        max_result = max(results.keys())
        backtrack_index = results[max_result]
        return (max_result, backtrack_index)

    def calculate_s(self, sequence):
        sequence = tuple(sequence)
        result = 0
        for i in range(0, len(sequence)):
            for j in range(i+1, len(sequence)):
                if sequence[i] == sequence[j]:
                    result += NucleotideScoring.MATCH
                elif sequence[i] == '-' or sequence[j] == '-':
                    result += NucleotideScoring.GAP
                else:
                    result += NucleotideScoring.MISMATCH
        return result

    def generate_traceback_path(self):
        # start from the end of hypercube
        current_index = self.hypercube.dimensions
        current_index = [k - 1 for k in current_index]
        current_index = tuple(current_index)

        # trace back to the beginning
        traceback_path = list()
        while current_index != tuple([-1] * len(current_index)):
            current_segment = self.hypercube.data[current_index]
            traceback_path.append(current_segment.index)
            current_index = current_segment.get_backtrack_index()[0]
        traceback_path.append(current_index)

        return traceback_path

    def generate_output(self, traceback_path):
        output_sequences = list()
        while len(output_sequences) < self.hypercube.dimension_count:
            output_sequences.append(list())

        sequence_indices = list(self.hypercube.dimensions)
        previous_step = traceback_path[0]
        for current_step in traceback_path[1:]:
            indices_changed = Hypercube.compare_indices(previous_step, current_step)
            previous_step = tuple(current_step)
            for i in range(0, len(indices_changed)):
                if indices_changed[i]:
                    sequence_indices[i] -= 1
                    if sequence_indices[i] >= 0:
                        output_sequences[i].insert(0, self.hypercube.sequences[i][sequence_indices[i]])
                    else:
                        output_sequences[i].insert(0, '-')
                else:
                    output_sequences[i].insert(0, '-')
        return output_sequences
