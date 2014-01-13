class HypercubeSegment:
    def __init__(self, index, nucleotides, distance=0):
        self.index = tuple(index)
        self.nucleotides = tuple(nucleotides)
        self.__distance = distance
        self.__backtrack_index = None

    def get_distance(self):
        return self.__distance

    def set_distance(self, distance):
        self.__distance = distance

    def get_backtrack_index(self):
        return self.__backtrack_index

    def set_backtrack_index(self, value):
        self.__backtrack_index = value


class Hypercube:
    def __init__(self, sequences):
        self.size = 1
        self.dimensions = list()

        for sequence in sequences:
            self.dimensions.append(len(sequence))
            # +1 for outer shell of the hypercube which contains initial values
            self.size *= len(sequence) + 1

        print ("INFO: generating hypercube with {0} dimensions ({1} segments).".format(self.dimensions, self.size))

        self.sequences = tuple(sequences)
        self.dimension_count = len(self.dimensions)
        self.transform_list = self.__generate_transform_list()

        # generate hypercube data structure
        index = [-1] * self.dimension_count
        self.data = dict()
        self.sorted_keys = list()
        while len(self.data) < self.size:
            # outer shell
            if -1 in index:
                self.data[tuple(index)] = HypercubeSegment(index, ['-'] * self.dimension_count)
                backward_neighbors_indices = list()
                for neighbor in self.backward_neighbors(self.data[tuple(index)]):
                    backward_neighbors_indices.append(neighbor.index)
                self.data[tuple(index)].set_backtrack_index(backward_neighbors_indices)
            # real data
            else:
                nucleotides = list()
                for i in range(0, len(index)):  # len(indices) == len(sequences)
                    nucleotides.append(sequences[i][index[i]])
                self.data[tuple(index)] = HypercubeSegment(index, nucleotides)
            self.sorted_keys.append(tuple(index))
            index = self.next_index(index)

        print ("INFO: hypercube generated.")
        return

    # Generates transform list for calculating segment's neighbours
    # e.g. Transform (0, 1, 1) creates (i+0, j+1, k+1) index for Hypercube with 3 dimensions
    # e.g. Transform (1, 0, 1) creates (i+1, j+0, k+1) index for Hypercube with 3 dimensions
    def __generate_transform_list(self):
        transform_list = list()
        for i in range(1, 2 ** self.dimension_count):
            bit_string = bin(i).split('b')[1]
            transform_element = list(bit_string)
            transform_element = [int(el) for el in transform_element]

            while len(transform_element) < self.dimension_count:
                transform_element.insert(0, 0)

            transform_list.append(tuple(transform_element))
        return tuple(transform_list)

    def next_index(self, indices):
        for i in range(len(indices) - 1, -1, -1):
            if (indices[i] + 1) < self.dimensions[i]:
                indices[i] += 1
                break
            else:
                indices[i] = -1
        return indices

    def forward_neighbors(self, segment):
        assert isinstance(segment, HypercubeSegment)
        neighbors = list()
        for transform in self.transform_list:
            next_index = list()
            for i in range(0, len(segment.index)):
                next_index.append(segment.index[i] + transform[i])
            if self.data.has_key(tuple(next_index)):
                neighbors.append(self.data[tuple(next_index)])
        return neighbors

    def backward_neighbors(self, segment):
        assert isinstance(segment, HypercubeSegment)
        neighbors = list()
        for transform in self.transform_list:
            previous_index = list()
            for i in range(0, len(segment.index)):
                previous_index.append(segment.index[i] - transform[i])
            if self.data.has_key(tuple(previous_index)):
                neighbors.append(self.data[tuple(previous_index)])
            #else:  # initial values outside the hypercube
            #    neighbors.append(HypercubeSegment(previous_index, tuple(), max(previous_index)+1))
        return neighbors

    # Returns tuple of booleans with size = len(index1)
    # False for each component of index1 that is different in index2
    @staticmethod
    def compare_indices(index1, index2):
        if len(index1) != len(index2):
            raise ValueError

        result = list()
        for i in range(0, len(index1)):
            if index1[i] == index2[i]:
                result.append(False)
            else:
                result.append(True)

        return tuple(result)